# WebApp

## Introduction

Web is made in blazor web assembly, so it comes with all the advantages and disadvantages of wasm.

## Development

For new environment variables, add them to the `src/WebApp/wwwroot/appsettings.json` file.
Then add a new line in `src/WebApp/Properties/update-env.sh` to update the environment variables on the build and use
the docker compose `.env`.