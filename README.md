# Docker Setup
 1. Download docker if not already installed. Use the following links based on you Ubuntu OS version
 
    [Ubuntu 12 64 bit](https://apt.dockerproject.org/repo/pool/main/d/docker-engine/docker-engine_17.04.0~ce-0~ubuntu-precise_amd64.deb)
                  
    [Ubuntu 14 64 bit](https://download.docker.com/linux/ubuntu/dists/trusty/pool/stable/amd64/docker-ce_17.03.2~ce-0~ubuntu-trusty_amd64.deb)
    
    [Ubuntu 16 64 bit](https://download.docker.com/linux/ubuntu/dists/xenial/pool/stable/amd64/docker-ce_17.03.2~ce-0~ubuntu-xenial_amd64.deb)

 
 2. Install docker
  *Run the command below change the path to your downloaded the  Docker package*

    ```sh
    sudo dpkg -i /path/to/package.deb
    ```
 4. Upgrade docker upgrade to latest version (***for Ubuntu 14 and 16 Only***)
    ```sh
    sudo apt-get update
    sudo apt-get upgrade docker-ce
    ``` 
    
# DWAPI Setup
#### a) New Installation
 Install DWAPI
 ```sh
sudo docker run --name dwapi -p 5757:5757 -d --restart unless-stopped kenyahmis/dwapi:latest
```

#### b) Upgrading Existing Installation
 Upgrading DWAPI to latest version
 ```sh
sudo docker pull kenyahmis/dwapi
sudo docker stop dwapi
sudo docker rm dwapi
sudo docker run --name dwapi -p 5757:5757 -d --restart unless-stopped kenyahmis/dwapi:latest
```

# MySQL Setup
1. Configure MySQL to allow remote access. Edit your my.cnf file which is found on
	**/etc/mysql/my.cnf**  OR  **/etc/mysql/mysql.conf.d/mysqld.cnf** depending on your mysql installation.
#### a)  MySQL 5.5
Change line bind-address = 127.0.0.1 to
```sh
#bind-address = 127.0.0.1
 ```
 #### b)  MySQL 5.6 - add the line if it does not exists
 ```sh
bind-address = *
```	
 2. Create a DWAPI database user for MySQL
 ```sh
 create user 'dwapi'@'%' identified by 'dwapi';
 ```
 3. Assign privileges to the DWAPI database user for MySQL
 ```sh
 GRANT ALL PRIVILEGES ON *.* TO 'dwapi'@'%' IDENTIFIED BY 'dwapi' WITH GRANT OPTION; 
 ```
 ```sh
FLUSH PRIVILEGES;
```

# Using DWAPI

1. Start DWAPI

On your browser open dwapi on `http://localhost:5757`

2. Configure your data sources and verify registries

> Please note that for the database connection will need to specify the IP address of the computer and **NOT** localhost or 127.0.0.1

3. Restart DWAPI
```sh
sudo docker restart dwapi
```

# Troubleshooting DWAPI
i)  View log files   
```sh
sudo docker exec -it dwapi ls logs
```
ii)  Copying log files folder to your pc current directory. 

```sh
sudo docker cp dwapi:/app/logs/ .
```

# Dwapi for Windows Instruction

1) Install prerequisite(NetCore Runtime)
  https://www.microsoft.com/net/download/dotnet-core/2.1
2) Install DWAPI
  http://data.kenyahmis.org:81/dwapi/client/downloads/dwapi.exe

# Dwapi Linux Installation (Ubuntu 64 bit 14 and above ONLY)

1) Install prerequisite(NetCore SDK)

open terminal (CTRL+ALT+T)

run the following commands as per you ubuntu dist

Ubuntu 14
wget -q https://packages.microsoft.com/config/ubuntu/14.04/packages-microsoft-prod.deb

Ubuntu 16
wget -q https://packages.microsoft.com/config/ubuntu/16.04/packages-microsoft-prod.deb

Ubuntu 18
wget -q https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb

sudo dpkg -i packages-microsoft-prod.deb

sudo apt-get install apt-transport-https

sudo apt-get update

sudo apt-get install dotnet-sdk-2.2

sudo apt-get wget

2) Install DWAPI
  
  option 1 form commandline
  
  wget -q data.kenyahmis.org:81/dwapi/client/downloads/dwapi.zip
  
  option 2 download from browser
  
  http://data.kenyahmis.org:81/dwapi/client/downloads/dwapi.zip

  unzip dwapi.zip
  
  go to unzipped dwapi folder
  
  cd dwapi
  
  start dwapi by running this command
  
  dotnet Dwapi.dll
  
  restarting dwapi
  
  close terminal
  
  go to unzipped dwapi folder
  
  start dwapi by running this command
  
  dotnet Dwapi.dll
