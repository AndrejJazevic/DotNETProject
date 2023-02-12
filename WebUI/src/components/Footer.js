import { Container, Row, Col, Card, ListGroup, ListGroupItem } from "react-bootstrap";
import { Link } from 'react-router-dom';
export default function Footer() {
    return (
        <footer>*
            <Container>
                <Row>
                    <Col sm={12} md={6}>
                        <h6>About</h6>
                        <p className="text-justify">
                        Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries
                        </p>
                    </Col>
                    <Col sm={12} md={6}>
                        <h6>Links</h6>
                        <Link to="/" className="nav-link">Home</Link>
                        <Link to="/login" className="nav-link">Login</Link>
                        <Link to="/" className="nav-link">Home</Link>
                        <Link to="/login" className="nav-link">Login</Link>
                    </Col>
                </Row>
            </Container>
        </footer>
    )
  }
  