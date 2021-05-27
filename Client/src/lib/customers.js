const API_PATH = process.env.REACT_APP_API_PATH;

export async function getAllCustomers() {
  const url = `${API_PATH}/customers`;
  const response = await fetch(url);
  const data = await response.json();

  if (!response.ok) {
    throw new Error(data.message || 'Could not fetch customers.');
  }

  return data;
}

export async function getSingleCustomer(customerId) {
  const url = `${API_PATH}/customers/${customerId}`;
  const response = await fetch(url); 
  const data = await response.json();
  if (!response.ok) {
    throw new Error(data.message || 'Could not fetch customer.');
  }

  return data;
}