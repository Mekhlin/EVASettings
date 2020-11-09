$modFile = "GameData/ExtendedEVA/Plugins/ExtendedEVA.dll"
$7zipPath = "$env:ProgramFiles\7-Zip\7z.exe"

if (-not (Test-Path -Path $modFile)) {
    throw "Files not found"
}

if (-not (Test-Path -Path $7zipPath -PathType Leaf)) {
    throw "7-Zip file '$7zipPath' not found"
}

$productVersion = (Get-Item $modFile).VersionInfo.ProductVersion

$v = $productVersion.split(".")
$version = "$($v[0]).$($v[1]).$($v[2])"

Set-Alias 7zip $7zipPath

$target = "release/ExtendedEVA-$($version).zip"
$source = "./GameData"
#7zip a -mx=9 $target $source
7zip a -tzip $target $source "README.md" "LICENSE"