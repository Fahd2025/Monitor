import { useEffect } from 'react';

import AppList from '../components/apps/AppList';
import LoadingSpinner from '../components/UI/LoadingSpinner';
import NoAppsFound from '../components/apps/NoAppsFound';
import useHttp from '../hooks/use-http';
import { getAllApps } from '../lib/apps';

const AllApps = () => {
  const { sendRequest, status, data: loadedApps, error } = useHttp(
    getAllApps,
    true
  );

  useEffect(() => {
    sendRequest();
  }, [sendRequest]);

  if (status === 'pending') {
    return (
      <div className='centered'>
        <LoadingSpinner />
      </div>
    );
  }

  if (error) {
    return <p className='centered focused'>{error}</p>;
  }

  if (status === 'completed' && (!loadedApps || loadedApps.length === 0)) {
    return <NoAppsFound />;
  }

  return <AppList apps={loadedApps} />;
};

export default AllApps;
