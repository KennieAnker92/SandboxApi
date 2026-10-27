import { Api } from './API';

export const api = new Api({
  // Use lowercase 'u' to properly override the generated client's default URL
  baseUrl: 'http://localhost:5011', 
});