param($tag,
    $registry,
    [switch]$arm64,
    [switch]$nobuild,
    [switch]$push
)

if ($null -eq $tag)
{
    $tag = "dev"
}
$platform = "linux/amd64"
if ($arm64)
{
    Write-Output "Building for arm64"
    $tag = "$tag-arm64"
    $platform = "linux/arm64"
}

if (Test-Path '.\.env')
{
    $envFile = Get-Content -Path '.\.env'
    foreach ($line in $envFile)
    {
        Write-Output $line
        $parts = $line -split '=', 2
        [System.Environment]::SetEnvironmentVariable($parts[0], $parts[1], 'Process')
    }
    $registry = "$env:REGISTRY/"
}
$api = "$( $registry )todos-api:$tag"
$webapp = "$( $registry )todos-webapp:$tag"
$builder = "$( $registry )todos-db-builder:$tag"

if ($nobuild -eq $false)
{
    Write-Host "Publishing $tag to $registry" -ForegroundColor Green
    Write-Output "Publishing Api"
    docker buildx build --platform $platform -f src/Api/Dockerfile -t $api .
    if ($LASTEXITCODE -ne 0)
    {
        Write-Error "Docker build for Api failed"
        exit 1
    }

    Write-Output "Publishing WebApp"
    docker buildx build --platform $platform -f src/WebApp/Dockerfile -t $webapp .
    if ($LASTEXITCODE -ne 0)
    {
        Write-Error "Docker build for WebApp failed"
        exit 1
    }

    Write-Output "Publishing Db builder"
    docker buildx build --platform $platform -f src/Persistence/Persistence.MigrationTool/Dockerfile -t $builder .
    if ($LASTEXITCODE -ne 0)
    {
        Write-Error "Docker build for Db builder failed"
        exit 1
    }
}

if ($push = $true)
{
    if ($null -ne $registry)
    {
        Write-Host "Publishing to registry" -ForegroundColor Green
        docker push $api
        docker push $webapp
        docker push $builder
    }
}

