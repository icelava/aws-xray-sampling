#!bin/bash

wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

sudo apt-get update; \
  sudo apt-get install -y apt-transport-https && \
  sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-3.1

  BUCKETURL=https://s3.ap-southeast-1.amazonaws.com/aws-xray-assets.ap-southeast-1
  wget $BUCKETURL/xray-daemon/aws-xray-daemon-3.x.deb
  sudo dpkg -i aws-xray-daemon-3.x.deb