import React from 'react';
import { Navigate, Route, Routes  } from 'react-router-dom';
import { router } from 'router/router';

const AppRouter = () => {
  return (
    <Routes>
      {router.map((route) => (
        <Route
          element={route.component}
          path={route.path}
          exact={route.exact}
          key={route.path}
        />
      ))}
    </Routes>
  );
};

export default AppRouter;
