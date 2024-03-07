echo "enter you nuget package name(such as package_name, not package_name.nupkg)"
set /p pkgname =
echo "enter you nuget api"
set /p api =
nuget push -Source https://api.nuget.org/v3/index.json %pkgname%.nupkg -ApiKey %api%