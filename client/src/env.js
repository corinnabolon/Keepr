export const dev = window.location.origin.includes('localhost')
export const baseURL = dev ? 'https://localhost:7045' : ''
export const useSockets = false
export const domain = 'corinnabolon.us.auth0.com'
export const audience = 'http://codeworksstudent.com'
export const clientId = 'FqB01hmTBRujQIyM1f5Owlcxjn1hcIwd'