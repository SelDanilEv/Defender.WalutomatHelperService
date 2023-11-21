docker rm -f WalutomatHelperService
docker build . -t walutomat-helper-service && ^
docker run -d --name WalutomatHelperService -p 49055:80 ^
--env-file ./../../secrets.list ^
-e ASPNETCORE_ENVIRONMENT=DockerDev ^
-it walutomat-helper-service
echo finish walutomat-helper-service
pause
