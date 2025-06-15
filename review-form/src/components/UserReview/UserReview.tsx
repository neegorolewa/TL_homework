import classes from './UserReview.module.css'
import Avatar from '../../../public/big-eyes-face.svg'

interface UserReviewProps {
    username: string;
    description: string;
    rating: number;
}

export default function UserReview({ username, description, rating }: UserReviewProps) {
    return (
        <div className={classes.container} >
            <div className={classes.avatarContainer}>
                <img
                    className={classes.avatar}
                    src={Avatar}
                    alt="Аватар пользователя"
                />
            </div>
            <div className={classes.contentContainer}>
                <div className={classes.header}>
                    <p className={classes.username}>{username}</p>
                    <p className={classes.rating}>{rating + '/5'}</p>
                </div>
                <p className={classes.description}>{description}</p>
            </div>
        </div >
    )
}