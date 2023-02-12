// src/components/logout-button.js

import React from 'react';
import { useAuth0 } from '@auth0/auth0-react';
import { Link } from 'react-router-dom';
import styled from 'styled-components';
import { Container, Navbar, Nav, Button } from 'react-bootstrap';

const SignInBtn = styled(Button)`
  font-family: 'Raleway', sans-serif;
  font-weight: 700;
  font-size: 13px;
  padding: 8px 16px;
`;

const LogoutButton = () => {
  const { logout } = useAuth0();
  return (
    <SignInBtn
      className='btn btn-danger btn-block'
      onClick={() =>
        logout({
          returnTo: window.location.origin
        })
      }
    >
      Log Out
    </SignInBtn>
  );
};

export default LogoutButton;
