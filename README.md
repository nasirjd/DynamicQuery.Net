# DynamicQuery.Net

for Querying an IQueryable in the normal approach you should write any predicates statically in your code, but if you want to have a dynamic Query you can use Expression Trees, this package used it!

if you search at this subject you will find  [DynamicQuery](https://www.nuget.org/packages/DynamicQuery) that is so exhaustive, but in performance, this package is faster than that more than two hundred percent!

# How to use

## [Install Nuget Package](https://www.nuget.org/packages/DynamicQuery.Net)
```
$ Install-Package DynamicQuery.Net
```

# What you can do:

## Dynamic Filtering:

### Creating FilterInput object:
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

### Using FilterInput object

Now we can use our filterInput variable:

```cs
myQueryable = myQueryable.Filter(filerInput);
```


## Dynamic Ordering

### Creating OrderInput object:

#### For a single field:
```cs
 var orderInput = new[]
            {
                new OrderInput {Order = OrderTypeEnum.Desc, Property = "Date"},
                new OrderInput {Order = OrderTypeEnum.Asc, Property = "Name"},
                new OrderInput {Order = OrderTypeEnum.Asc, Property = "ID"}
            };
```

#### For an array of fields:

```cs
 var orderInput = new[]
            {
                new OrderInput {Order = OrderTypeEnum.Desc, Property = "Date"},
                new OrderInput {Order = OrderTypeEnum.Asc, Property = "Name"},
                new OrderInput {Order = OrderTypeEnum.Asc, Property = "ID"}
            };
```

### Using OrderInput object:

```cs
myQueryable = myQueryable.Order(orderInput);
```

## Both of Filtering and Ordering:

### Creating and Using OrderFilterInput object

```cs
var orderFilterInput = new OrderFilterInput 
                {
                    Filter = filerInput,
                    Order = orderInput
                }
               
 myQueryable = myQueryable.Filter(orderFilterInput); 
```
In this release OrderFilterInput properties just supports array of objects.


## Create simple REST APIs:

### In the client side send a JSON to the server:

```json
{
    "Filter":[{"Property":"Date" , "Value":"2017/04/07" , "Type":"String" , "Operation":"GreaterThan"}],
    "Order":[{"Property":"Date" , "Order":"Desc"},{"Property":"ID" , "Order":"Desc"}]
}
```

### In the server just use it in .Filter() Method:

```cs
        public HttpResponseMessage Filter(OrderFilterInput orderFilterInput)
        {
            return Request.CreateResponse(HttpStatusCode.OK, myQueryable.Filter(orderFilterInput));
        }
```

I Hope this will be Helpful
