export const auth = {
    state: {
        isAuthenticated: false,
        currentUser: null,
    },

    init() {
        this.checkAuthStatus();
    },

    checkAuthStatus() {
        const savedUser = localStorage.getItem('jsonDiffUser');
        if (savedUser) {
            this.state.isAuthenticated = true;
            this.state.currentUser = savedUser;
        }
    },

    login(userName) {
        this.state.isAuthenticated = true;
        this.state.currentUser = userName;
        localStorage.setItem('jsonDiffUser', userName);
        return true;
    },

    logout() {
        this.state.isAuthenticated = false;
        this.state.currentUser = null;
        localStorage.removeItem('jsonDiffUser');
        return true;
    }
};