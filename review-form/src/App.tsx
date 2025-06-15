import { useState } from "react";
import ReviewForm from "./components/ReviewForm/ReviewForm";
import UserReview from "./components/UserReview/UserReview";
import type { ReviewData } from "./data/data";

export default function App() {
  const [data, setData] = useState<ReviewData | null>(null);

  const handleSumbitForm = ({username, description, rating}: ReviewData) => {
    setData({username, description, rating});
  }

  return (
    <>
      <ReviewForm onSubmit={handleSumbitForm}/>
      {data && (<UserReview username={data?.username} description={data?.description} rating={data?.rating}/>)}
    </>
  );
}

