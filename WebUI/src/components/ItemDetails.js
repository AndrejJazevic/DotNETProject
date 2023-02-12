import { Container, Row, Col, Card, ListGroup, ListGroupItem, Button, Carousel } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import { useHistory, useParams } from 'react-router';
import React from 'react';
import axios from 'axios';
import { useAuth0 } from '@auth0/auth0-react';

const serverUrl = process.env.REACT_APP_SERVER_URL;
const baseURL = `${serverUrl}/api/items`;

export default function ItemDetails() {
  const { id } = useParams();

  const { user } = useAuth0();

  const history = useHistory();

  const { getAccessTokenSilently } = useAuth0();

  const [post, setPost] = React.useState(null);

  const handleSubmit = async e => {
    e.preventDefault();
    const token = await getAccessTokenSilently();

    axios
      .delete(`${baseURL}/${id}`, {
        headers: {
          Authorization: `Bearer ${token}`
        }
      })
      .then(() => {
        alert('Post deleted!');
        setPost(null);
        history.push('/');
      });
  };

  React.useEffect(() => {
    axios.get(baseURL + `/${id}`).then(response => {
      setPost(response.data);
      console.log(response.data);
    });
  }, []);

  return (
    <Container>
      <Row xs={1} md={2} className='g-4'>
        <Col>
          {post != null && (
            <Card style={{ width: '18rem' }}>
              {post.imageUrls[0] == null ? (
                <div
                  style={{
                    height: '200px',
                    backgroundColor: 'grey'
                  }}
                ></div>
              ) : (
                <Carousel>
                  {post.imageUrls.map(image => (
                    <Carousel.Item>
                      <img className='d-block w-100' src={image} alt='First slide' />
                    </Carousel.Item>
                  ))}
                </Carousel>
              )}
              <Card.Body>
                <Card.Title>{post.name}</Card.Title>
                <Card.Text>{post.description}</Card.Text>
              </Card.Body>
              <ListGroup className='list-group-flush'>
                <ListGroupItem>
                  <b>City: </b>
                  {post.city}
                </ListGroupItem>
              </ListGroup>
              <Card.Body>
                {user != null && user.sub != null && post.uploader == user.sub ? (
                  <Button variant='danger' onClick={handleSubmit}>
                    Delete
                  </Button>
                ) : (
                  <Button variant='success'>Take item</Button>
                )}
              </Card.Body>
            </Card>
          )}
        </Col>
        ))
      </Row>
    </Container>
  );
}
