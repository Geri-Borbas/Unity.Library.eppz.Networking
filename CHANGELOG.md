# eppz.Networking


* 0.0.6

	+ Removed `Define` dependency
	+ Manually renamed `FRAMEWORK` macros to detect Unity (`UNITY_5_0_OR_NEWER || UNITY_2017_1_OR_NEWER`)

* 0.0.5

	+ Examples with performance sadbox
		+ `LagMeter`
		+ `Requester`

* 0.0.4

	+ Examples in `Scenes`

* 0.0.3
		
	+ Hotfixed a `RegEx` issue
		+ `RegexOptions.Compiled` was not available
		+ Using `UNITY_5_0_OR_NEWER` and `UNITY_2017_1_OR_NEWER` compiler macros

* 0.0.2

	+ Added RestSharp `105.2.3` (last release not using C#4 features)
		+ Added automatic define `UNITY_5_0_OR_NEWER || UNITY_2017_1_OR_NEWER`
			+ Sets RestSharp dependencies just right
		+ Removed `csproj` and `AssemblyInfo` files

* 0.0.1

	+ Initial commit