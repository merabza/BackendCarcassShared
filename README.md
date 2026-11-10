# BackendCarcassShared

## SSO mode (merabza/SsoLtgGe#6)

- `BffRoutes`: the `/bff/*` paths of an app host in SSO mode and the CSRF header (`X-CSRF: 1`).
- `BffUserResponse`: the answer of `GET /bff/user` (user, roles, app claims, logout form token); it replaces
  `LoginResponse` in SSO mode, and no tokens reach the browser.
- `ClientCredentialsTokenProvider`: a machine client's access token from sso.ltg.ge (`<Authority>connect/token`,
  client_credentials), cached until 60 s before it expires.
- `JwtContractReCounterApiClient`: a constructor taking a `ClientCredentialsTokenProvider`, and
  `AuthenticateWithClientCredentials()`, which sets the token for the next requests and the message hub. The
  user name and password login stays for Local mode.
