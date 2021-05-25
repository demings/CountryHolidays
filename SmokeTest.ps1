$url = "https://countryholidays20210525111327.azurewebsites.net/"

Invoke-WebRequest -UseBasicParsing -URI ($url + "countries")
Invoke-WebRequest -UseBasicParsing -URI ($url + "ago/2020")
Invoke-WebRequest -UseBasicParsing -URI ($url + "ltu/2021/02/16")
Invoke-WebRequest -UseBasicParsing -URI ($url + "est/2022/max")
