docker rm -f DevWalutomatHelperService
docker build . -t dev-walutomat-helper-service && ^
docker run -d --name DevWalutomatHelperService -p 49055:80 ^
--env-file ./../../secrets/secrets.dev.list ^
-e ASPNETCORE_ENVIRONMENT=DockerDev ^
-it dev-walutomat-helper-service
echo finish dev-walutomat-helper-service
docker image prune -f
pause
