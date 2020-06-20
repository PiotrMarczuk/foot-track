sudo gpsd /dev/ttyACM0 -F /var/run/gpsd.socket
stty -F /dev/ttyACM0 -echo
sudo systemctl stop gpsd.socket
sudo systemctl disable gpsd.socket
sudo killall gpsd
sudo systemctl stop gpsd.socket
sudo systemctl disable gpsd.socket
sudo gpsd /dev/ttyACM0 -F /var/run/gpsd.socket
sleep 10s
nohup python /home/pi/sendCoordinate.py > pylog.test &