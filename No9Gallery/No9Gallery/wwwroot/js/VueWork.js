//var comment = new Vue({
//    el: '#comment_list_component',
//    data() {
//        return {
//            comments: [],
//            isViewReady: false
//        };
//    },
//    methods: {
//        refreshData: function () {
//            var self = this;
//            this.isViewReady = false;

//            //UPDATED TO GET DATA FROM WEB API
//            axios.get('/api/PictureInfo/GetPictureInfo/')
//                .then(function (response) {
//                    self.comments = response.data;
//                    self.isViewReady = true;
//                })
//                .catch(function (error) {
//                    alert("ERROR: " + (error.message | error));
//                });
//        }
//    },
//    created: function () {
//        this.refreshData();
//    }
//});

var show_photo = new Vue({
    el: '#show_photo',
    data() {
        return {
            work_photo: '',
            works: []
        };
    },
    methods: {
        refreshData: function () {
            var self = this;

            //UPDATED TO GET DATA FROM WEB API
            axios.get('/api/PictureInfo/GetPictureInfo/')
                .then(function (response) {
                    self.work = response.data;
                    self.work_photo = "/image/works/" + self.work[0].picture;
                })
                .catch(function (error) {
                    alert("ERROR: " + (error.message | error));
                });
        }
    },
    created: function () {
        this.refreshData();
    }
});

//获取标题
var HeadlineComponent = new Vue({
    el: '#headline-component',
    data() {
        return {
            works: {}
        };
    },
    methods: {
        refreshData: function () {
            var self = this;

            //UPDATED TO GET DATA FROM WEB API
            axios.get('/api/pictureinfo/GetPictureInfo/')
                .then(function (response) {
                    self.works = response.data[0];
                })
                .catch(function (error) {
                    alert("ERROR: " + (error.message | error));
                });
        }
    },
    created: function () {
        this.refreshData();
    }
})

//获取介绍
var IntroductionComponent = new Vue({
    el: '#introduction-component',
    data() {
        return {
            works: {}
        };
    },
    methods: {
        refreshData: function () {
            var self = this;

            //UPDATED TO GET DATA FROM WEB API
            axios.get('/api/pictureinfo/GetPictureInfo/')
                .then(function (response) {
                    self.works = response.data[0];
                })
                .catch(function (error) {
                    alert("ERROR: " + (error.message | error));
                });
        }
    },
    created: function () {
        this.refreshData();
    }
})

//展示头像
var avatar_component = new Vue({
    el: '#avatar_component',
    data() {
        return {
            head_photo: "/image/work_res/head_portrait.png",
            avatar_ID: ""
        };
    },
    methods: {
        refreshData: function () {
            var self = this;

            //UPDATED TO GET DATA FROM WEB API
            axios.get('/api/PictureInfo/GetPictureInfo/')
                .then(function (response) {
                    self.avatar_ID = "/image/avatar/" + response.data[0].user_ID;
                })
                .catch(function (error) {
                    alert("ERROR:0980978970896 " + (error.message | error));
                });

            //UPDATED TO GET DATA FROM WEB API
            axios.get('/api/PictureInfo/GetHead/')
                .then(function (response) {
                    self.head_photo = "/image/avatar/" + response.data;
                })
                .catch(function (error) {
                    alert("ERROR: " + (error.message | error));
                });
        }
    },
    created: function () {
        this.refreshData();
    }
});

//获取作者名
var UserComponent = new Vue({
    el: '#user-component',
    data() {
        return {
            works: {}
        };
    },
    methods: {
        refreshData: function () {
            var self = this;

            //UPDATED TO GET DATA FROM WEB API
            axios.get('/api/pictureinfo/GetPictureInfo/')
                .then(function (response) {
                    self.works = response.data[0];
                })
                .catch(function (error) {
                    alert("ERROR: " + (error.message | error));
                });
        }
    },
    created: function () {
        this.refreshData();
    }
})

//获取标签及跳转界面
var LabelComponent = new Vue({
    el: '#label_component',
    data() {
        return {
            label: []
        };
    },
    methods: {
        refreshData: function () {
            var self = this;

            //UPDATED TO GET DATA FROM WEB API
            axios.get('/api/pictureinfo/gettypes/')
                .then(function (response) {
                    self.label = response.data;
                })
                .catch(function (error) {
                    alert("ERROR: " + (error.message | error));
                });
        }
    },
    created: function () {
        this.refreshData();
    }
});

//获取点赞数
var likesComponent = new Vue({
    el: '#likes_component',
    data() {
        return {
            works: [],
            likes_photo: "/image/work_res/like.png",
            isLiked: false,
            is_login: false,
            likes: 0
        };
    },
    methods: {
        refreshData: function () {
            var self = this;

            axios.get('/api/pictureinfo/iflogin/')
                .then(function (response) {
                    self.is_login = response.data;

                    if (self.is_login == true) {
                        axios.get('/api/pictureinfo/ifLiked/')
                            .then(function (response) {
                                self.isLiked = response.data;
                                if (self.isLiked == true) {
                                    self.likes_photo = "/image/work_res/liked.png";
                                    document.getElementById("likenum").style.color = "#FFFFFF";
                                }
                                else if (self.isLiked == false) {
                                    self.likes_photo = "/image/work_res/like.png";
                                }
                            })
                            .catch(function (error) {
                                alert("ERROR888888888: " + (error.message | error));
                            });
                    }
                })
                .catch(function (error) {
                    alert("error11111111111");
                });
            //UPDATED TO GET DATA FROM WEB API
            axios.get('/api/pictureinfo/getpictureinfo/')
                .then(function (response) {
                    self.works = response.data;
                    self.likes = response.data[0].likes_num;

                })
                .catch(function (error) {
                    alert("ERROR: " + (error.message | error));
                });

            
        },
        addLikes: function () {
            var self = this;


            if (self.isLiked == false) {
                self.likes_photo = "/image/work_res/liked.png";
                axios.get('/api/pictureinfo/addlikes')
                    .then(function (response) {
                        self.likes = self.likes + 1;
                        self.isLiked = true;
                        document.getElementById("likenum").style.color = "#FFFFFF";
                    })
                    .catch(function (error) {
                        alert("ERROR: " + (error.message | error));
                    });
            }
        }
    },
    created: function () {
        this.refreshData();
    }
})


//关注
//关注
var FollowComponent = new Vue({
    el: '#follow-component',
    data() {
        return {
            isFollowed: false,
            is_login: false,
            follow_photo: "/image/work_res/follow.png"

        };
    },
    methods: {
        refreshData: function () {
            var self = this;

            //UPDATED TO GET DATA FROM WEB API
            axios.get('/api/pictureinfo/iflogin/')
                .then(function (response) {
                    self.is_login = response.data;
                })
                .catch(function (error) {
                    alert("error11111111111");
                });
            if (self.is_login==true) {
            axios.get('/api/pictureinfo/iffollowed')
                .then(function (response) {
                    self.isFollowed = response.data;
                    if (self.isFollowed == true)
                        self.follow_photo = "/image/work_res/followed.png";
                    else if (self.isFollowed == false)
                        self.follow_photo = "/image/work_res/follow.png";
                })
                .catch(function (error) {
                    alert("ERROR:" + (error.message | error));
                });
            }
            
        },
        follow: function () {
            var self = this;

            //UPDATED TO GET DATA FROM WEB API

            if (self.isFollowed == false) {
                self.follow_photo = "/image/work_res/followed.png";
                axios.get('/api/pictureinfo/followaction')
                    .then(function (response) {

                    })
                    .catch(function (error) {
                        alert("ERROR:22222 " + (error.message | error));
                    });
            }

        }
    },
    created: function () {
        this.refreshData();
    }
})


//获取收藏数
var CollectComponent = new Vue({
    el: '#collect-component',
    data() {
        return {
            works: [],
            is_login: false,
            collect_photo: "/image/work_res/collect.png",
            isCollected: false,
            collect: 0
        };
    },
    methods: {
        refreshData: function () {
            var self = this;
            axios.get('/api/pictureinfo/iflogin/')
                .then(function (response) {
                    self.is_login = response.data;

                    if (self.is_login) {
                        axios.get('/api/pictureinfo/ifcollected/')
                            .then(function (response) {
                                self.isCollected = response.data;
                                if (self.isCollected == true) {
                                    self.collect_photo = "/image/work_res/collected.png";
                                    document.getElementById("collectnum").style.color = "#FFFFFF";
                                }
                                else {
                                    self.collect_photo = "/image/work_res/collect.png";
                                    
                                }
                            })
                            .catch(function (error) {
                                alert("ERROR: " + (error.message | error));
                            });
                    }
                })
                .catch(function (error) {
                    alert("error11111111111");
                });
            
            //UPDATED TO GET DATA FROM WEB API
            axios.get('/api/pictureinfo/getpictureinfo/')
                .then(function (response) {
                    self.works = response.data;
                    self.collect = response.data[0].collect_num;
                })
                .catch(function (error) {
                    alert("ERROR: " + (error.message | error));
                });

            
        },
        addCollect: function () {
            var self = this;


            if (self.isCollected == false) {
                self.collect_photo = "/image/work_res/collected.png";
                document.getElementById("collectnum").style.color = "#FFFFFF";

                axios.get('/api/pictureinfo/addcollections')
                    .then(function (response) {
                        self.collect = self.collect + 1;
                        self.isCollected = true;
                    })
                    .catch(function (error) {
                        alert("ERROR: " + (error.message | error));
                    });
            }

        }
    },
    created: function () {
        this.refreshData();
    }
})


//下载
var DownloadComponent = new Vue({

    el: '#download-component',

    data() {
        return {
            is_login: false,
            isPointsEnough: false
        };
    },
    methods: {
        refreshData: function () {
            var self = this;
            axios.get('/api/pictureinfo/iflogin/')
                .then(function (response) {
                    self.is_login = response.data;
                })
                .catch(function (error) {
                    alert("error11111111111");
                });
        },
        ifEnoughPoint: function () {
            var self = this;
            axios.get('/api/pictureinfo/iflogin/')
                .then(function (response) {
                    self.is_login = response.data;
                })
                .catch(function (error) {
                    alert("error11111111111");
                });

            axios.get('/api/pictureinfo/ifenoughpoints/').then(function (response) {
                self.isPointsEnough = response.data;

                if (self.isPointsEnough == false) alert("Your points are not enough. Please recharge quickly!");
                else {
                    alert("Download successfully!");


                    axios.get('/api/pictureinfo/decreasepoints/').then(function (response) {

                    }).catch(function (error) {
                        alert("ERROR: " + (error.message | error));
                    })
                }
            })
                .catch(function (error) {
                    alert("ERROR: " + (error.message | error));
                });
        }
    },
    created: function () {
        this.refreshData();
    }

})

//获取下载积分
var PointComponent = new Vue({
    el: '#point-component',
    data() {
        return {
            works: []
        };
    },
    methods: {
        refreshData: function () {
            var self = this;

            //UPDATED TO GET DATA FROM WEB API
            axios.get('/api/pictureinfo/getpictureinfo/')
                .then(function (response) {
                    self.works = response.data;
                })
                .catch(function (error) {
                    alert("ERROR: " + (error.message | error));
                });
        }
    },
    created: function () {
        this.refreshData();
    }
})



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
            axios.post('/api/subscribers/getall/', {
                new_comment: this.new_comment,
            })
                .then(function (response) {
                    console.log(response);
                });
        }
    }
});

var work = new Vue({
    el: '#work_photo',
    data() {
        return {
            work_photo: 'photo'
        };
    },
    methods: {
        refreshData: function () {
            var self = this;

            //UPDATED TO GET DATA FROM WEB API
            axios.get('/api/subscribers/getall/')
                .then(function (response) {
                    self.work_photo = response.data;
                })
                .catch(function (error) {
                    alert("ERROR: " + (error.message | error));
                });
        }
    }
});

var uuuwork = new Vue({
    el: '#test_photo',
    data() {
        return {
            testwork_photo: "/image/work_res/testPhoto.png"
        };
    }
});


//举报
var ReportComponent = new Vue({
    el: '#report-component',
    data() {
        return {

        };
    },
    methods: {
        addReport: function () {

            axios.get('/api/pictureinfo/addreport/')
                .then(function (response) {
                    alert("You have complained the work!");
                })
                .catch(function (error) {
                    alert("ERROR: " + (error.message | error));
                });

        }
    }

})

//var comment = new Vue({
//    el: '#comment_list_component',
//    data() {
//        return {
//            comments: [],
//            comment_avatar:[],
//            //fstatus: 100,
//            isViewReady: false
//        };
//    },
//    methods: {
//        refreshData: function () {
//            var self = this;
//            this.isViewReady = false;

//            //UPDATED TO GET DATA FROM WEB API
//            axios.get('/api/PictureInfo/GetCommentInfo/')
//                .then(function (response) {
//                    self.comments = response.data;
//                    //self.fstatus = response.status;
//                    for (i_res in response.data) {
//                        self.comment_avatar.push("/image/avatar/" + i_res.avatar);
//                    }
//                    self.isViewReady = true;
//                })
//                .catch(function (error) {
//                    alert("ERROR: " + (error.message | error));
//                });
//        }
//    },
//    created: function () {
//        this.refreshData();
//    }
//});


