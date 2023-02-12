import { useState, useEffect } from 'react';
import { Container, Row } from 'react-bootstrap';
import styled from 'styled-components';
import axios from 'axios';
import Listings from '../components/listings/Listings';

const serverUrl = process.env.REACT_APP_SERVER_URL;
const baseURL = `${serverUrl}/api/items`;

const StyledRow = styled(Row)`
    margin-top: 16px;
`;

const Home = () => {
    const [post, setPost] = useState(null);

    useEffect(() => {
        axios
            .get(baseURL)
            .then(res => setPost(res.data))
            .catch(error => console.log(error));
    }, []);

    return (
        <Container>
            <StyledRow>
                <Listings list={post} />
            </StyledRow>
        </Container>
    );
};

export default Home;
