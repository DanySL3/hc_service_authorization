@echo off

PowerShell.exe -Command "docker build -t andessuiteidentity ."
PowerShell.exe -Command "docker run --restart=always -p 5012:5012 -e ASPNETCORE_URLS=http://+:5012 -e TOKEN_SECRET='SENfU2VydmljZV9Ub2tlbkFjY2Vzc0FwcGxpY2FjaW9u' -e DB_COREBANK_SERVER='192.168.1.250\sql2012' -e DB_COREBANK_DATABASE='SI_BDDesarrollo_MN00' -e DB_COREBANK_USER='AcumdLP/wascrNUch9j98rvjAqy97pg8aVHRrjxUhb0csOaBWefeyMpkCoOK+zaJjo5a+xjhU7W1pvALTo9Uy2l5vRZywpo0In+9KYtZc6o=' -e DB_COREBANK_PASSWD='AcumdLP/wascrNUch9j98rvjAqy97pg8aVHRrjxUhb0csOaBWefeyMpkCoOK+zaJjo5a+xjhU7W1pvALTo9Uy2l5vRZywpo0In+9KYtZc6o=' --name andessuiteidentity -d andessuiteidentity"

pause
goto begin