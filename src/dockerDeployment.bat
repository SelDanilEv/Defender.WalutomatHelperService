docker rm -f ServiceTemplate
docker build . -t service-template && ^
docker run -d --name ServiceTemplate -p 0000:80 ^
--env-file ./../../secrets.list ^
-e ASPNETCORE_ENVIRONMENT=DockerDev ^
-it service-template
echo finish service-template
pause
