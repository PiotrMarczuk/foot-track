from sys import argv
from azure.iot.device import IoTHubDeviceClient, Message

import gpsd
import requests
import time

# az iot hub device-identity show-connection-string --hub-name {YourIoTHubName} --device-id MyNodeDevice --output table
CONNECTION_STRING = "connectionString"

# Define the JSON message to send to IoT Hub.
MSG_TXT = '{{"lat": {lat},"lon": {lon}}}'

def iothub_client_init():
    # Create an IoT Hub client
    client = IoTHubDeviceClient.create_from_connection_string(CONNECTION_STRING)
    return client

def iothub_client_run():

    gpsd.connect()

    try:
        client = iothub_client_init()
        print ( "IoT Hub device sending periodic messages, press Ctrl-C to exit" )

        while True:
            try :
                packet = gpsd.get_current()
                msg_txt_formatted = MSG_TXT.format(lat = packet.lat, lon = packet.lon)
                message = Message(msg_txt_formatted)
                print( "Sending message: {}".format(message) )
                client.send_message(message)
                print ( "Message successfully sent" )
                time.sleep(1)
            except Exception as e :
              print("Got exception " + str(e))
    except KeyboardInterrupt:
        print ( "IoTHubClient stopped" )

if __name__ == '__main__':
    print ( "IoTHubClient running" )
    print ( "Press Ctrl-C to exit" )
    iothub_client_run()
