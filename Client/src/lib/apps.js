const API_PATH = process.env.REACT_APP_API_PATH;

export async function getAllApps() {
  const url = `${API_PATH}/apps`
  const response = await fetch(url);
  const data = await response.json();

  if (!response.ok) {
    throw new Error(data.message || 'Could not fetch apps.');
  }

  return data;
}

export async function getSingleApp(appId) {
  const url = `${API_PATH}/apps/${appId}`;
  const response = await fetch(url);
  const data = await response.json();

  if (!response.ok) {
    throw new Error(data.message || 'Could not fetch app.');
  }
  return data;
}
