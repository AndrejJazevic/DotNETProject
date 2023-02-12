import { Skeleton, ImageDiv, Image, InformationDiv, Title, Uploader, Icon } from './CardSkeleton.styles';

const CardSkeleton = () => {
    return (
        <div>
            <ImageDiv>
                <Skeleton animation='glow'>
                    <Image />
                </Skeleton>
            </ImageDiv>
            <InformationDiv>
                <div>
                    <Skeleton animation='glow'>
                        <Title />
                    </Skeleton>
                    <Skeleton animation='glow'>
                        <Uploader />
                    </Skeleton>
                </div>
                <div>
                    <Skeleton animation='glow'>
                        <Icon />
                    </Skeleton>
                </div>
            </InformationDiv>
        </div>
    );
};

export default CardSkeleton;
