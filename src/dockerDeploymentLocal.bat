docker rm -f LocalWalutomatHelperService
docker build . -t local-walutomat-helper-service && ^
docker run -d --name LocalWalutomatHelperService -p 47055:80 ^
--env-file ./../../secrets/secrets.local.list ^
-e ASPNETCORE_ENVIRONMENT=DockerLocal ^
-it local-walutomat-helper-service
echo finish local-walutomat-helper-service
docker image prune -f
pause
