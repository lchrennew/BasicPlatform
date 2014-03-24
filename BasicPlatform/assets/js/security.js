!function () {
    var upper = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ',
    lower = 'abcdefghijklmnopqrstuvwxyz',
    num = '0123456789',
    ch = '`-=~!@#$%^&*()_+[]\{}|;\':",./<>?'
    String.prototype.getPwdLv = function () {
        var s = 0
        if (!this.length);
        else if (this.length < 5) s += 5
        else if (this.length > 4 && this.length < 8) s += 10
        else if (this.length > 7) s += 25

        var upperHit = this.hit(upper), lowerHit = this.hit(lower), alphaHit = upperHit + lowerHit
        if (upperHit == 0 && lowerHit != 0) s += 10
        else if (upperHit != 0 && lowerHit != 0) s += 20


        var numHit = this.hit(num)
        if (numHit == 1) s += 10
        if (numHit >= 3) s += 20

        var chHit = this.hit(ch)
        if (chHit == 1) s += 10
        if (chHit > 1) s += 25

        if (numHit != 0 && alphaHit != 0) s += 2
        if (numHit != 0 && alphaHit != 0 && chHit != 0) s += 3
        if (numHit != 0 && upperHit != 0 && lowerHit != 0 && chHit != 0) s += 5
        return s
    }
    String.prototype.hit = function (charset) {
        if (charset) {
            var count = 0
            for (var i = 0; i < this.length; i++)
                if (charset.indexOf(this.charAt(i)) > -1) count++
            return count
        }
        return 0
    }
}()