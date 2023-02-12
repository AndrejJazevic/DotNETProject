import { useState } from 'react';
import axios from 'axios';
import { useAuth0 } from '@auth0/auth0-react';
import { Container, Form, Button } from 'react-bootstrap';

const serverUrl = process.env.REACT_APP_SERVER_URL;
const baseURL = `${serverUrl}/api/items`;

const AddListing = () => {
	const [images, setImages] = useState<FileList>();
	const { getAccessTokenSilently } = useAuth0();

	const sendRequest = async (e: any) => {
		e.preventDefault();

		const token = await getAccessTokenSilently();
		const formData = new FormData(e.target);
		const { name, description, category, city } = Object.fromEntries(
			formData.entries()
		);

		const data = new FormData();
		data.append('name', name);
		data.append('description', description);
		data.append('category', category);
		data.append('city', city);

		if (images != null) {
			for (let i = 0; i < images.length; i++) {
				data.append('postedFiles', images[i]);
			}
		}

		try {
			await axios.post(baseURL, data, {
				headers: {
					Authorization: `Bearer ${token}`,
				},
			});
		} catch (e) {
			console.log(e);
		}
	};

	return (
		<Container>
			<Form onSubmit={sendRequest} style={{ width: '20rem' }}>
				<Form.Group className='mb-3'>
					<Form.Label>Name</Form.Label>
					<Form.Control
						type='text'
						name='name'
						placeholder='Example: Sofa'
					/>
				</Form.Group>

				<Form.Group className='mb-3'>
					<Form.Label>Description</Form.Label>
					<Form.Control
						as='textarea'
						rows={3}
						name='description'
						placeholder='Example: Not used anymore'
					/>
				</Form.Group>

				<Form.Group className='mb-3'>
					<Form.Label>
						Which category your item suits the most?
					</Form.Label>
					<Form.Control as='select' name='category'>
						<option>Please select category</option>
						<option value='1'>Furniture</option>
						<option value='2'>Misc</option>
						<option value='3'>Technology</option>
						<option value='4'>Other</option>
					</Form.Control>
				</Form.Group>

				<Form.Group className='mb-3'>
					<Form.Label>City</Form.Label>
					<Form.Control
						type='text'
						name='city'
						placeholder='Example: Vilnius'
					/>
				</Form.Group>

				<Form.Group controlId='formFileMultiple' className='mb-3'>
					<Form.Label>Images</Form.Label>
					<Form.Control
						type='file'
						onChange={(e: any) => setImages(e.target.files)}
						multiple
					/>
				</Form.Group>

				<Button variant='primary' type='submit'>
					Submit
				</Button>
			</Form>
		</Container>
	);
};

export default AddListing;
