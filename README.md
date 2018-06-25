# Project status
| Branch | Build        | Quality | 
| ------------- | ------------- | ------------- |
| master | ![Build Status](https://shoshins.visualstudio.com/_apis/public/build/definitions/3ee635a9-029b-40d9-a9b6-93cc7737dbf9/1/badge)      | ![Code Quality](https://sonarcloud.io/api/project_badges/measure?project=apple-receipt-parser&metric=alert_status) |

# Nuget Packages Information

## Apple Receipt Models

### Description
Describes strongly-type representation of Apple Receipt Object.
[Apple Documentation](https://developer.apple.com/library/archive/releasenotes/General/ValidateAppStoreReceipt/Chapters/ReceiptFields.html)

### Nuget information 
[Link to package](https://www.nuget.org/packages/Apple.Receipt.Models/)

| Version | Downloads |
| ------------- | ------------- |
| ![NuGet](https://img.shields.io/nuget/v/Apple.Receipt.Models.svg) | ![NuGet](https://img.shields.io/nuget/dt/Apple.Receipt.Models.svg) |

### Installation:
* (Package manager): ```Install-Package Apple.Receipt.Models```
* (.Net CI): ```dotnet add package Apple.Receipt.Models```
* (Packet CLI): ```paket add Apple.Receipt.Models```

## Apple Receipt Parser

### Description
Parser for Apple Receipt that represented in base64 and encoded with ASN.1
[Anatomy of a Receipt payload encoded with ASN.1](https://www.objc.io/issues/17-security/receipt-validation/)

### Nuget information 
[Link to package](https://www.nuget.org/packages/Apple.Receipt.Parser/)

| Version | Downloads |
| ------------- | ------------- |
| ![NuGet](https://img.shields.io/nuget/v/Apple.Receipt.Parser.svg) | ![NuGet](https://img.shields.io/nuget/dt/Apple.Receipt.Parser.svg) |

### Installation:
* (Package manager): ```Install-Package Apple.Receipt.Parser```
* (.Net CI): ```dotnet add package Apple.Receipt.Parser```
* (Packet CLI): ```paket add Apple.Receipt.Parser```

### How to use:
```

// Register DI module...
builder.RegisterModule<AppleReceiptParserModule>();
...
// ... and resolve the service later.
IAppleReceiptParserService parserService;
...
// Get your base64 Apple Receipt
const string appleAppReceipt = "{receipt_base64_string}";
// Convert to Bytes
byte[] data = Convert.FromBase64String(appleAppReceipt);
// Get parsed receipt
AppleAppReceipt receipt = parserService.GetAppleReceiptFromBytes(data);
```

## Apple Receipt Verificator

### Description
Apple Receipt Validator using Apple App Store. 
Two step verification: pre-validation that can be customized and App Store verification.
[Apple Receipt Validation with App Store documentation](https://developer.apple.com/library/archive/releasenotes/General/ValidateAppStoreReceipt/Chapters/ValidateRemotely.html)

### Nuget information 
[Link to package](https://www.nuget.org/packages/Apple.Receipt.Verificator/)

| Version | Downloads |
| ------------- | ------------- |
| ![NuGet](https://img.shields.io/nuget/v/Apple.Receipt.Verificator.svg) | ![NuGet](https://img.shields.io/nuget/dt/Apple.Receipt.Verificator.svg) |

### Installation:
* (Package manager): ```Install-Package Apple.Receipt.Verificator```
* (.Net CI): ```dotnet add package Apple.Receipt.Verificator ```
* (Packet CLI): ```paket add Apple.Receipt.Verificator```

### How to use:
```
// (Optional) You can create implementation of custom validation process:
containerBuilder.RegisterType<AppleReceiptCustomVerificatorService>()
                .As<IAppleReceiptCustomVerificatorService>();
...
// Fill settings:
            AppleReceiptVerificationSettings settings = new AppleReceiptVerificationSettings(
            "XXXX", // Apple Shared Secret Key
            AppleReceiptVerificationType.Sandbox, // Verification Type: Sandbox / Production
            new[] {"com.mbaasy.ios.demo"}, // Array with allowed bundle ids
                new LoggerConfiguration(), // Serilog configuration
                true); // Enabled / Disabled logger registration (use it when you already configured serilog in application)
// Register DI module...
AppleReceiptVerificatorModule module = new AppleReceiptVerificatorModule(settings);
            containerBuilder.RegisterModule(module);
...
// ... and resolve the service later.
IAppleReceiptVerificatorService verificator;
...
AppleReceiptVerificationResult result = await verificator.VerifyAppleReceiptAsync(appleAppReceipt);
```
