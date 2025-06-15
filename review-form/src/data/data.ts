export type EmojiButtonData = {
    src: string;
    alt: string;
};

export type EmojiButtonList = {
    rate: number;
    emoji: EmojiButtonData;
};

export type ReviewData = {
    username: string;
    description: string;
    rating: number;
}