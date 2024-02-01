tag=$1
registry=$2

echo "Publishing " $tag " to " $registry

#echo "Publishing Api"
#docker buildx build --platform linux/arm64 -f src/Api/Dockerfile -t registry.treize.cloud/todos-api:0.9 .
#docker push registry.treize.cloud/todos-api:0.9
#
#echo "Publishing WebApp"
#docker buildx build --platform linux/arm64 -f src/WebApp/Dockerfile -t registry.treize.cloud/todos-webapp:0.9 .
#docker push registry.treize.cloud/todos-api:0.9
#
#echo "Publishing Db builder"
#docker buildx build --platform linux/arm64 -f src/Persistence/Persistence.MigrationTool/Dockerfile -t registry.treize.cloud/todos-db-builder:0.9 .
#docker push registry.treize.cloud/todos-db-builder:0.9