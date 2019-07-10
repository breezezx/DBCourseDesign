var comment = new Vue({
    el: '#comment_list_component',
    data(){
        return {
            comments: [],
            isViewReady: false
        };
    },
    methods: {
        refreshData: function () {
            var self = this;
            this.isViewReady = false;

            //UPDATED TO GET DATA FROM WEB API
            axios.get('/api/PictureInfo/getcommentinfo/')
                .then(function (response) {
                    self.comments = response.data;
                    self.isViewReady = true;
                })
                .catch(function (error) {
                    alert("ERROR: " + (error.message | error));
                });
        }
    }
  
});

var comment_publish = new Vue({
    el: '#comment_publish',
    data() {
        return {
            new_comment: "",
            isViewReady: false
        };
    },
    methods: {
        sendcomment() {
            var self = this;
            this.isViewReady = false;

            //UPDATED TO GET DATA FROM WEB API
            axios.post('/api/PictureInfo/GetPictureInfo/',this.new_comment)
                .then(function (response) {
                    console.log(response);
                });
        }
    }
});

var show_photo = new Vue({
    el: '#show_photo',
    data() {
        return {
            work_photo: "/image/work_res/testPhoto.png"
        };
    },
    methods: {
        refreshData: function () {
            var self = this;

            //UPDATED TO GET DATA FROM WEB API
            axios.get('/api/PictureInfo/GetPictureInfo/')
                .then(function (response) {
                    self.work_photo = response.data.picture;
                })
                .catch(function (error) {
                    alert("ERROR: " + (error.message | error));
                });
        }
    }
});

//展示头像
var show_head = new Vue({
    el: '#show_head',
    data() {
        return {
            head_photo: "/image/work_res/head_portrait.png"
        };
    },
    methods: {
        refreshData: function () {
            var self = this;

            //UPDATED TO GET DATA FROM WEB API
            axios.get('/api/PictureInfo/GetHead/')
                .then(function (response) {
                    self.head_photo = response.data;
                })
                .catch(function (error) {
                    alert("ERROR: " + (error.message | error));
                });
        }
    }
});

