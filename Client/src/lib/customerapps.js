const API_PATH = process.env.REACT_APP_API_PATH;

export async function getAllCustomerApps(customerId) {
  let url = `${API_PATH}`;
  if(customerId){ 
    url += `?customerId=${customerId}`;
  }
  const response = await fetch(url);
  const data = await response.json();  
  if (!response.ok) {
    throw new Error(data.message || 'Could not fetch customers.');
  }

  return data.data;
}

export async function getSingleCustomerApp(customerAppId) {
  const url = `${API_PATH}/${customerAppId}`;
  const response = await fetch(url); 
  const data = await response.json();
  if (!response.ok) {
    throw new Error(data.message || 'Could not fetch customer.');
  }

  return data;
}