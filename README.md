1) Download docker
--------------
[Ubuntu 12 64 bit](https://apt.dockerproject.org/repo/pool/main/d/docker-engine/docker-engine_17.04.0~ce-0~ubuntu-precise_amd64.deb)

[Ubuntu 14 64 bit](https://download.docker.com/linux/ubuntu/dists/trusty/pool/stable/amd64/docker-ce_17.03.2~ce-0~ubuntu-trusty_amd64.deb)

[Ubuntu 17 64 bit](https://download.docker.com/linux/ubuntu/dists/xenial/pool/stable/amd64/docker-ce_17.03.2~ce-0~ubuntu-xenial_amd64.deb)

2) Install docker
---
*changing the path below to the path where you downloaded the Docker package*
- - -
***sudo dpkg -i /path/to/package.deb***
- - -

3) Install DWAPI
---
- - -
***sudo docker run --name dwapi -p 5757:5757 -d --restart unless-stopped kenyahmis/dwapi:latest***
- - -

4) Update your browser
---
***sudo apt-get update***
***sudo apt-get install firefox***

5) Configure MySQL
---
>Edit your my.cnf file, which usually lives on /etc/mysql/my.cnf on Unix/OSX systems. In some cases the location for the file is /etc/mysql/mysql.conf.d/mysqld.cnf).

>Change line bind-address = 127.0.0.1 to #bind-address = 127.0.0.1

 >run this SQL command locally:

 >GRANT ALL PRIVILEGES ON *.* TO 'dwapi'@'%' IDENTIFIED BY 'dwapi' WITH GRANT OPTION;
 FLUSH PRIVILEGES;

7) Start DWAPI
---
On your browser open dwapi on `http://localhost:5757`
Configure your data sources and verify registries

8) Restart DWAPI
---
- - -
***sudo docker restart dwapi***
- - -


Troubleshooting DWAPI
--------------

i) View log files   
---

 ***sudo docker exec -it dwapi ls logs***

ii) Copying log files folder to your pc current directory.  
---

***sudo docker cp dwapi:/app/logs/ .***

# Dwapi for Windows Instruction

1) Install prerequisite(NetCore Runtime)
  https://www.microsoft.com/net/download/dotnet-core/2.1
2) Install DWAPI
  http://data.kenyahmis.org:81/dwapi/client/downloads/dwapi.exe


