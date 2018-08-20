# Dwapi for Linux Instruction

1) Download docker
--------------
[Ubuntu 12 64 bit](https://apt.dockerproject.org/repo/pool/main/d/docker-engine/docker-engine_17.04.0~ce-0~ubuntu-precise_amd64.deb)

[Ubuntu 14 64 bit](https://download.docker.com/linux/ubuntu/dists/trusty/pool/stable/amd64/docker-ce_17.03.2~ce-0~ubuntu-trusty_amd64.deb)

[Ubuntu 16 64 bit](https://download.docker.com/linux/ubuntu/dists/xenial/pool/stable/amd64/docker-ce_17.03.2~ce-0~ubuntu-xenial_amd64.deb)

2) Install docker
---
*changing the path below to the path where you downloaded the Docker package*
- - -
***sudo dpkg -i /path/to/package.deb***
- - -

3) Install and Start DWAPI
---
- - -
***sudo docker run --name dwapi -p 5757:5757 -d --restart unless-stopped kenyahmis/dwapi:latest***
- - -

Update your browser and open dwapi on `http://localhost:5757`
Configure your data sources and verify registries
For **MySql** 
> * Host should be the **IP of machine** and not localhost or 127.0.0.1
> * Server should allow remote access


4) Restart DWAPI
---
- - -
***sudo docker restart dwapi***
- - -

# Dwapi for Windows Instruction

1) Install prerequisite(NetCore Runtime)
  https://www.microsoft.com/net/download/dotnet-core/2.1
2) Install DWAPI
  http://data.kenyahmis.org:81/dwapi/client/downloads/dwapi.exe


