# MoneyLion Technical Assessment

This assessment would like to manage users’ accesses to new features via feature switches, i.e. enabling/disabling certain feature based on a user’s email and feature names.

GET /feature?email=XXX&featureName=XXX

This endpoint receives email (user’s email) and featureName as request parameters and returns the following response in JSON format.

Example Response:

```json
{
	"canAccess": true|false 
	//(will be true if the user has access to the featureName
}
```

## Usage

```cs
[HttpGet]
public async Task<ActionResult<clsCanAccess>> GetFeatureAccess(string email, string featureName)
{
	var fa = await _context.FeatureAccesses.Where(x => x.Email == email && x.FeatureName == featureName).ToListAsync();

	if (fa == null)
	{
		return NotFound();
	}
	if (fa.Count >= 1)
	{
		if (fa.FirstOrDefault().Enable == true)
			return new clsCanAccess { canAccess = true };
		else
			return new clsCanAccess { canAccess = false };
	}
	return NotFound();
}
```

POST /feature

This endpoint receives the following request in JSON format and returns an empty response with HTTP Status OK (200) when the database is updated successfully, otherwise returns https Status Not Modified (304).


Example Request:

```json
{
	"featureName": "xxx", (string)
	"email": "xxx", (string) (user's name)
	"enable": true|false (boolean) (uses true to enable a user's access, otherwise
}
```


## Usage

```cs
[HttpPost]
public async Task<ActionResult<FeatureAccess>> PostFeatureAccess(FeatureAccess featureAccess)
{
	try
	{
		_context.FeatureAccesses.Add(featureAccess);
		await _context.SaveChangesAsync();
	}
	catch (Exception ex)
	{
		if (ex.InnerException.Message.Contains("Cannot insert duplicate key"))
		{
			return StatusCode(304);
		}
	}
	return Ok();
}
```


