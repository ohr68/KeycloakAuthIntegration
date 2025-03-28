[Back to README](../README.md)

## Keycloak config

This section ensures that the configuration is made properly so that the authentication and jwt validation processes work properly.


## Client creation 
 - Click on the Clients menu, and then click on the Create Client button.
 - Let's create it with the name web-api and then click on Next.
 - On the second screen, enable the Client authentication option and click Save.

## Configuring client on the Api and Consumers projects
 # Keycloak
 - Click on the Clients menu
 - Click on the web-api
 - Click on the Credentials tab
 - Click on the copy button of the "Client Secret"
 
 # Projects
 - Go to the appsettings.json
 - On Keycloak:credentials:secret
 - Replace it with the copied value


## Client Scope
 - Click on the Client scopes menu, and then click Create Client Scope.
 - The scope must be named web-api-scope
 - Set "Type: Default"
 - Set "Protocol: OpenId Connect"
 - Set "Include in token scope: On"
 - Click Save
 - Click on the "Mappers" tab and click on "Create a new mapper"
 - Select "By configuration"
 - Click on the Audience option
 - Name it web-api-audience
 - List the web-api client in the Included Client Audience option.
 - Set "Add to access token: On"
 - Click Save
 - Click Add Mapper

## Custom Attribute
 - Go to Realm Settings > User profile 
 - Click Create Attribute
 - Name it "custom_id"
 - Set Display name "Custom Id"
 - Set "Enabled when: Always"
 - Set "Required field: On"
 - Set "Required for: Only users"
 - Set "Required when: always"
 - Set "Who can edit?: Admin"
 - Set "Who can view?: Admin"
 - Click Save
 
## Adding custom attribute to the token
 - Click on the Client scopes menu
 - Click on the web-api-scope
 - Click on the Mappers tab
 - Click Add mapper > By configuration
 - Select User Attribute
 - Name id custom-id
 - Set User Attribute "custom_id"
 - Set Token claim name "customId"
 - Set Claim JSON Type "String"
 - Set "Add to ID token: On"
 - Set "Add to access token: On"
 - Set "Add to user info: On"
 - Set "Add to token introspection: On"
 - Click Save
 
## Adding user id to the token
 - Click on the Client scopes menu
 - Click on the web-api-scope
 - Click on the Mappers tab
 - Click Add mapper > By configuration
 - Select User Property
 - Name it user-id
 - Set Property "id"
 - Set Token claim name "userId"
 - Set Claim JSON Type "String"
 - Set "Add to ID token: On"
 - Set "Add to access token: On"
 - Set "Add to user info: On"
 - Set "Add to token introspection: On"
 - Click Save
 