let manage = {}

manage.user = {}
manage.user.init = function () {
    rest.get(consts.urls.baseUrl +'manage/user/getAll',
        function onSuccess() {
            alert('success');
        },
        function onFailed() {
            alert('failed');
        });
}