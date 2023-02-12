import { ImageDiv, InformationDiv, Image, Information, Title, Uploader, Stats } from './Card.styles';
import styles from './Card.module.css';
import HeartButton from './HeartButton';

interface CardProps {
    title: string;
    uploader: string;
    image?: string;
}

const defaultProps: CardProps = {
    title: 'Unspecified item',
    uploader: 'Unknown uploader'
};

const Card = (props: CardProps) => {
    return (
        <div>
            <ImageDiv>
                <Image src={props.image} />
            </ImageDiv>
            <InformationDiv>
                <Information>
                    <Title className={styles.wrapText}>{props.title}</Title>
                    <Uploader className={styles.wrapText}>{props.uploader}</Uploader>
                </Information>
                <Stats>
                    <HeartButton />
                </Stats>
            </InformationDiv>
        </div>
    );
};

Card.defaultProps = defaultProps;

export default Card;
