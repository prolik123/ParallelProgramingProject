import matplotlib.pyplot as plt

plt.xlabel('Number of threads')
plt.ylabel('Time')
plt.title('Performace')
plt.plot([1, 2, 3], [10, 5, 20], color='red')
plt.plot([1, 2, 3], [3, 10, 15], color='blue')
plt.legend(['szybki', 'wolny'])
plt.show()