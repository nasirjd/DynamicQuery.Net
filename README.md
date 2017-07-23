# DynamicQuery.Net

for Querying an IQueryable in the normal approach you should write any predicates statically in your code, but if you want to have a dynamic Query you can use Expression Trees, this package used it!

if you search at this subject you will find  [DynamicQuery](https://www.nuget.org/packages/DynamicQuery) that is so exhaustive, but in performance, this package is faster than that more than two hundred percent!

# How to use

## [Install Nuget Package](https://www.nuget.org/packages/DynamicQuery.Net)
```
$ Install-Package DynamicQuery.Net
```

## Dynamic Filtering:

Creating FilterInput object:

```cs
var filerInput = new FilterInput
                {
                    Operation = OperationTypeEnum.GreaterThan,
                    Property = "Date",
                    Type = InputTypeEnum.String,
                    Value = "2017/04/08"
                };
```
if you want to filter more than one field , you can create a List of FilterInput objects
```cs
var filerInput = new List<FilterInput>
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

if you want to have more than one value for a single field, you can feed Value with a List: 
```cs
var filerInput = new List<FilterInput>
            {
                new FilterInput
                {
                    Operation = OperationTypeEnum.NotEqual,
                    Property = "ClassNo",
                    Type = InputTypeEnum.Number,
                    Value = new List<object>{2,3,4}
                }
            };
```

Using FilterInput object

Now we can use our filterInput variable:

```cs
myQueryable = myQueryable.Filter(filerInput);
```


## Dynamic Ordering

Creating OrderInput object:

For a single field:
```cs
 var orderItem = new OrderInput {Order = OrderTypeEnum.Desc, Property = "Date"};
```

For a List of fields:

```cs
 var orderInput = new List<OrderInput>
            {
                new OrderInput {Order = OrderTypeEnum.Desc, Property = "Date"},
                new OrderInput {Order = OrderTypeEnum.Asc, Property = "Name"},
                new OrderInput {Order = OrderTypeEnum.Asc, Property = "ID"}
            };
```

Using OrderInput object:

```cs
myQueryable = myQueryable.Order(orderInput);
```

## Both of Filtering and Ordering:

Creating and Using OrderFilterInput object

```cs
var orderFilterInput = new OrderFilterInput 
                {
                    Filter = filerInput,
                    Order = orderInput
                }
               
 myQueryable = myQueryable.Filter(orderFilterInput); 
```

## OrderFilterNonFilterInput 

If you want to send some objects to the server that you won't use it as a Filter in IQueryable, you can use NonFilter Dictionary.

```cs

var nonFilterInput = new Dictionary<string, string>
            {
                {"TestName1", "TestValue1"},
                {"TestName2", "TestValue2"},
                {"TestName3", "TestValue3"}
            };

            var orderFilterNonFilterInput = new OrderFilterNonFilterInput()
            {
                Order = orderInput,
                Filter = filterInput,
                NonFilter = nonFilterInput
            };
		myQueryable = myQueryable.Filter(orderFilterNonFilterInput);
```

## PagingInput

If you want to use paging in your filtering you can use PagingInput object :

```cs
 var paging = new PagingInput
            {
                Page = 2,
                Size = 10
            };
			
	myQueryable = myQueryable.Paging(paging);
```

## DynamicQueryNetInput

All of the above-mentioned capabilities can be achieved by using a DynamicQueryNetInput object as a parameter to the Filter() extension method:

```cs
         var dynamicQueryNetInput = new DynamicQueryNetInput()
            {
                Order = orderInput,
                Filter = filterInput,
                NonFilter = nonFilterInput,
                Paging = paging
            };
			
	myQueryable = myQueryable.Filter(dynamicQueryNetInput);
	
```


## Create simple REST APIs:

 In the client side send a JSON to the server:

```json
	{
      "Filter":[{"Property":"ContactNumber" , "Value":[2,3,4] , "Type":"Number" , "Operation":"Equal"}],
    	"Order":[{"Property":"Date" , "Order":"Desc"}],
    	"NonFilter":{"Calculate":"True"},
        "Paging":{"Page":3 , "Size":10}
	}
```

 In the server just use it in .Filter() Method:

```cs
  public HttpResponseMessage Filter(DynamicQueryNetInput dynamicQueryNetInput)
  {
      return Request.CreateResponse(HttpStatusCode.OK, myQueryable.Filter(dynamicQueryNetInput));
  }
```


 <h4 style="text-align: center;">I Hope this will be Helpful</h4>

