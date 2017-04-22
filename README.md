# DynamicQuery.Net
Dynamic filtering for IQueryable collections in C#.net

#How to use

## [Install Nuget Package](https://www.nuget.org/packages/DynamicQuery.Net)
```
$ Install-Package DynamicQuery.Net
```
## Create a new FilterInput
```cs
var filerInput = new FilterInput
                {
                    Operation = OperationTypeEnum.GreaterThan,
                    Property = "Date",
                    Type = InputTypeEnum.String,
                    Value = "2017/04/08"
                };
```
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

now we can use our filterInput variable



```cs
myQueryable = myQueryable.Filter(filerInput);
```



I Hope this will be Helpful
