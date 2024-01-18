all:
	dotnet sonarscanner begin /k:"WhiteBear" /d:sonar.host.url="http://localhost:9000" /d:sonar.token="sqp_02999edeefe6b8acc0c6ab6bf0455008f07d04ec" /d:sonar.cs.vscoveragexml.reportsPaths=".\coverage.xml"
	dotnet build --no-incremental
	dotnet-coverage collect 'dotnet test' -f xml  -o '.\coverage.xml'
	dotnet sonarscanner end /d:sonar.token="sqp_02999edeefe6b8acc0c6ab6bf0455008f07d04ec"