function I2=drawCircle(I1)
    R=I1;
    G=I1;
    B=I1;
    imshow(I1);
    [x,y]=size(I1);
    [c,r]=ginput(2);
    radius=sqrt((r(2)-r(1))^2+(c(2)-c(1))^2);
    for i=1:x
        for j=1:y
            if sqrt((i-r(1))^2+(j-c(1))^2)==radius
                R(i,j)=255;
                G(i,j)=0;
                B(i,j)=0;
            end
            
        end
    end
    I2=cat(3,R,G,B);
end