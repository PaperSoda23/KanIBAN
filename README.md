# KanIBAN

# Instructions
## use docker (prefered)
* caution: requires 'Docker Desktop' for Windows
* in root project dir run command `docker-compose up`

## use powershell
caution: requires .NET installed locally

### run app
1. MOUSE2 on run.ps1 in root project dir
2. select 'Run with PowerShell'

### test app
1. MOUSE2 on test.ps1 in root project dir
2. select 'Run with PowerShell'

# Endpoints

## root: `localhost:5000`

## endpoint single iban `post: /IBAN`
```
{
    "iban": "LT012345678901234567"
}
```

## endpoint multiple ibans `post: /IBAN/list`
```
{
  "ibans" [
            "AA012345678901234567",
            "LT017044078901234567",
            "LT012132178901234567",
            "LT012143278901234567",
            "LT012345678901234567",
            "LT01234567890123456712345",
            "BB012345678901234567"
      ]
}
```
