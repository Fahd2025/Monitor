import { Fragment, useEffect } from 'react';
import { useParams } from 'react-router-dom';

import HighlightedApp from '../components/apps/HighlightedApp';
import useHttp from '../hooks/use-http';
import { getSingleApp } from '../lib/apps';
import LoadingSpinner from '../components/UI/LoadingSpinner';

const AppDetail = () => {
  const params = useParams();

  const { appId } = params;

  const { sendRequest, status, data: loadedApp, error } = useHttp(
    getSingleApp,
    true
  );

  useEffect(() => {
    sendRequest(appId);
  }, [sendRequest, appId]);

  if (status === 'pending') {
    return (
      <div className='centered'>
        <LoadingSpinner />
      </div>
    );
  }

  if (error) {
    return <p className='centered'>{error}</p>;
  }

  if (!loadedApp.name) {
    return <p>No app found!</p>;
  }

  return (
    <Fragment>
      <HighlightedApp name={loadedApp.name} description={loadedApp.description} />
     
    </Fragment>
  );
};

export default AppDetail;
