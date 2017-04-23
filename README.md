# DynamicQuery.Net

for Querying an IQueryable in the normal approach you should write any predicates statically in your code, but if you want to have a dynamic Query you can use Expression Trees, this package used it!

if you search at this subject you will find  [DynamicQuery](https://www.nuget.org/packages/DynamicQuery) that is so exhaustive, but in performance, this package is faster than that more than two hundred percent!

# How to use

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

if you want to have more than one value for a single field, you can feed Value with an array: 
```cs
var filerInput = new FilterInput[]
            {
                new FilterInput
                {
                    Operation = OperationTypeEnum.NotEqual,
                    Property = "ClassNo",
                    Type = InputTypeEnum.Number,
                    Value = new[]{2,3,4}
                }
            };
```

## Filter IQueryable

Now we can use our filterInput variable:



```cs
myQueryable = myQueryable.Filter(filerInput);
```



I Hope this will be Helpful
