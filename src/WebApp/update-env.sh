
cat /usr/share/nginx/html/appsettings.json | jq --arg aVar "$(printenv ASPNETCORE_ENVIRONMENT)" '.Environment = $aVar' > /usr/share/nginx/html/appsettings.json
cat /usr/share/nginx/html/appsettings.json | jq --arg aVar "$(printenv API_URL)" '.API_URL = $aVar' > /usr/share/nginx/html/appsettings.json
