# DynamicQuery.Net
Dynamic filtering for IQueryable collections in C#.net

#How to use

## [Install Nuget Package](https://www.nuget.org/packages/DynamicQuery.Net)
```
$ Install-Package DynamicQuery.Net
```
## Create a new FilterInput

if you want to filter more than one field , you can create an array of FilterInput objects
```cs
var filerInput = new FilterInput[]
            {
                new FilterInput
                {
                    Operation = OperationTypeEnum.GreaterThan,
                    Property = "Date",
                    Type = InputTypeEnum.String,
                    Value = "2017/04/08"
                }
            };
```
## Filter IQueryable

```cs
myQueryable.Filter(filerInput);
```

