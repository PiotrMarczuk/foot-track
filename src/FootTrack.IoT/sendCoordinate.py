import threading
import time
import gpsd
from azure.iot.device import IoTHubDeviceClient, Message, MethodResponse
from datetime import datetime

CONNECTION_STRING = "connString"

MSG_TXT = '{{"latitude": "{lat}","longitude": "{lon}", "speed": "{speed}", "timestamp":"{timestamp}"}}'

SEND_GPS = False

def start_message_listener(client):
    global SEND_GPS
    while True:
        method_request = client.receive_method_request("startTraining")
        SEND_GPS = True
        resp_status = 200
        method_response = MethodResponse(method_request.request_id, resp_status)
        client.send_method_response(method_response)

def end_message_listener(client):
    global SEND_GPS
    while True:
        method_request = client.receive_method_request("endTraining")
        SEND_GPS = False
        resp_status = 200
        method_response = MethodResponse(method_request.request_id, resp_status)
        client.send_method_response(method_response)

def iothub_client_run():
    try:
        client = IoTHubDeviceClient.create_from_connection_string(CONNECTION_STRING)
        start_message_listener_thread = threading.Thread(target=start_message_listener, args=(client,))
        start_message_listener_thread.daemon = True
        start_message_listener_thread.start()
        end_message_listener_thread = threading.Thread(target=end_message_listener, args=(client,))
        end_message_listener_thread.daemon = True
        end_message_listener_thread.start()
        gpsd.connect()
        while True:
            if SEND_GPS:
                try :
                    packet = gpsd.get_current()
                    msg_txt_formatted = MSG_TXT.format(lat = packet.lat,lon = packet.lon, speed = packet.speed(), timestamp = datetime.utcnow())
                    message = Message(msg_txt_formatted)
                    print( "Sending message: {}".format(message) )
                    client.send_message(message)
                    print ( "Message successfully sent." )
                    time.sleep(1)
                except Exception as e :
                    print("Got exception " + str(e))
    except KeyboardInterrupt:
        print ( "Messaging device stopped." )

if __name__ == '__main__':
    print ( "Starting messaging device..." )
    iothub_client_run()