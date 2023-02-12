// src/components/login-button.js

import React from 'react';
import { useAuth0 } from '@auth0/auth0-react';
import styled from 'styled-components';
import { Container, Navbar, Nav, Button } from 'react-bootstrap';

const SignInBtn = styled(Button)`
  font-family: 'Raleway', sans-serif;
  font-weight: 700;
  font-size: 13px;
  padding: 8px 16px;
`;

const LoginButton = () => {
  const { loginWithRedirect } = useAuth0();
  return <SignInBtn onClick={() => loginWithRedirect()}>Log In</SignInBtn>;
};

export default LoginButton;
