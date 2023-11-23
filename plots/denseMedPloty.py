import matplotlib.pyplot as plt

plt.xlabel('Number of threads')
plt.ylabel('Time [ms]')
plt.title('Performace [|V| = 5e3, |E| = 1,2e6]')
plt.plot([1, 2, 4, 8, 16], [1450, 1450, 1450, 1450, 1450], color='red')
plt.plot([1, 2, 4, 8, 16], [1700, 1000, 700, 560, 510], color='blue')
plt.plot([1, 2, 4, 8, 16], [1710, 1000, 690, 480, 560], color='green')
plt.plot([1, 2, 4, 8, 16], [1600, 920, 650, 520, 480], color='orange')
plt.legend(['Single thread', 'Threads', 'Thread Pool', 'Lock data structures + Thread Pool'])
plt.show()