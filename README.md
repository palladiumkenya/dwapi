DWAPI
-------
![.NET Core](https://github.com/koskedk/dwapi/workflows/.NET%20Core/badge.svg?branch=master)


# Running DWAPI with docker

If you do not have Docker installed, visit https://docs.docker.com/get-started/#download-and-install-docker and choose your preferred operating system to download and install Docker


# DWAPI Setup
#### a) New Installation
1. Install DWAPI
 ```sh
sudo docker run --name dwapi -p 5757:5757 -p 5753:5753 -d --restart unless-stopped kenyahmis/dwapi:latest
```

#### b) Upgrading Existing Installation
1. Upgrading DWAPI to latest version
 ```sh
sudo docker ps -a | grep "dwapi" | awk '{print $1}' | xargs sudo docker rm -f
sudo docker images -a | grep "dwapi" | awk '{print $3}' | xargs sudo docker rmi
sudo docker run --name dwapi -p 5757:5757 -p 5753:5753 -d --restart unless-stopped kenyahmis/dwapi:latest
```

# MySQL Setup
1. Configure MySQL to allow remote access. Edit your my.cnf file which is found on
   **/etc/mysql/my.cnf**  OR  **/etc/mysql/mysql.conf.d/mysqld.cnf** depending on your mysql installation.

#### a)  MySQL 5.5

Change line bind-address = 127.0.0.1 to #bind-address = 127.0.0.1

#### b)  MySQL 5.6 - add the line if it does not exists
		bind-address = *

#### b)  MySQL 8 - add the line if it does not exists
		bind-address = 0.0.0.0
Incase there are some issues binding addred in mysql version 8, try the steps below
```sh
sudo nano /etc/mysql/mysql.conf.d/mysqld.cnf    or sudo gedit /etc/mysql/mysql.conf.d/mysqld.cnf

update this two lines  : -     bind-address             = 127.0.0.1                                                                                                                                                       		mysqlx-bind-address     = 127.0.0.1
to this   : -     bind-address           = 0.0.0.0                                                                                                                                                          	  mysqlx-bind-address    = 0.0.0.0       
restart mysql
```

2. Create a DWAPI database user for MySQL
```sh
 create user 'dwapi'@'%' identified by 'dwapi';
```
3. Assign privileges to the DWAPI database user for MySQL version 5.6
```sh
GRANT ALL PRIVILEGES ON *.* TO 'dwapi'@'%' IDENTIFIED BY 'dwapi' WITH GRANT OPTION; 
FLUSH PRIVILEGES;
```
if using mysql version 8
```sh
GRANT ALL PRIVILEGES ON *.* TO 'dwapi'@'%' WITH GRANT OPTION; 
FLUSH PRIVILEGES;
```

# Using DWAPI

1. Start DWAPI

On your browser open dwapi on `https://localhost:5753`

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
  https://data.kenyahmis.org:444/dwapi/client/downloads/dwapi.exe


