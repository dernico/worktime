
export const config: any = {
  authority: "https://accounts.google.com",
  client_id: "793729558350-58etvsoelqbc8pi5lknlven67esr03vh.apps.googleusercontent.com",
  client_secret: "jjgBcDv28Uqz-VaEueBX4Gwb",
  redirect_uri: "http://localhost:4200/callback.html",
  response_type: "id_token token", // id_token token / id_token / token / code
  scope: "openid profile",
  post_logout_redirect_uri: "http://localhost:4200/index.html",
  signoutRedirect: "http://localhost:4200/index.html",
  silent_redirect_uri: 'http://localhost:4200/silent-renew.html',
  automaticSilentRenew: true,
  silentRequestTimeout: 5000 //default
};